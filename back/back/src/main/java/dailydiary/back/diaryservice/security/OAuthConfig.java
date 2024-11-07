package dailydiary.back.diaryservice.security;

import org.springframework.beans.factory.annotation.Value;
import org.springframework.context.annotation.Configuration;

@Configuration
public class OAuthConfig {

    @Value("${oauth.client-id}")
    private String clientId;

    @Value("${oauth.client-secret}")
    private String clientSecret;

    public String getClientId() {
        return clientId;
    }

    public String getClientSecret() {
        return clientSecret;
    }
}
