package dailydiary.back.diaryservice.domain.user;

import lombok.Data;
import lombok.NoArgsConstructor;
import org.springframework.data.annotation.Id;
import org.springframework.data.mongodb.core.mapping.Document;

@Document(collection = "users")  // MongoDB의 'users' 컬렉션에 저장
@Data                             // getter, setter, toString, equals, hashCode 자동 생성
@NoArgsConstructor                // 기본 생성자 자동 생성
public class User {
    @Id
    private String email;              // 사용자 이메일 (primary key 역할)
    private String accessToken;        // 액세스 토큰

    // 필요에 따라 특정 생성자를 추가할 수 있지만 현재 Lombok 애노테이션으로 자동 생성됩니다.
}
